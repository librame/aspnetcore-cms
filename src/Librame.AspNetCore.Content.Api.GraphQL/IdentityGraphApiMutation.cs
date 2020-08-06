#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using Microsoft.AspNetCore.Content;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Encodings.Web;

namespace Librame.AspNetCore.Content.Api
{
    using AspNetCore.Api;
    using AspNetCore.Content.Api.Models;
    using AspNetCore.Content.Api.ModelTypes;
    using AspNetCore.Content.Api.Resources;
    using AspNetCore.Content.Stores;
    using Extensions;
    using Extensions.Core.Combiners;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Localizers;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;
    using Extensions.Network.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ContentGraphApiMutation<TUser, TGenId, TCreatedBy> : ObjectGraphType, IGraphApiMutation
        where TUser : class, IIdentifier<TGenId>, ICreation<TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        [InjectionService]
        private ILogger<ContentGraphApiMutation<TUser, TGenId, TCreatedBy>> _logger = null;


        public ContentGraphApiMutation(IInjectionService injectionService)
        {
            injectionService.Inject(this);

            Name = nameof(ISchema.Mutation);

            AddLoginTypeField();

            AddRegisterTypeField();
        }


        private void AddLoginTypeField()
        {
            FieldAsync<CategoryType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<LoginApiModel>("user");

                    var result = await _signInManager.PasswordSignInAsync(model.Email,
                        model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAndResultAsync();

                    if (result.Succeeded)
                    {
                        model.Message = "User logged in.";
                    }
                    if (result.RequiresTwoFactor)
                    {
                        model.Message = "Need requires two factor.";
                        model.RedirectUrl = "./LoginWith2fa";
                    }
                    if (result.IsLockedOut)
                    {
                        model.Message = "User account locked out.";
                        model.RedirectUrl = "./Lockout";
                    }
                    else
                    {
                        model.IsError = true;
                        model.Message = "Invalid login attempt.";
                    }

                    return model.Log(_logger);
                }
            );
        }

        private void AddRegisterTypeField()
        {
            FieldAsync<RegisterType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<RegisterApiModel>("user");

                    var user = typeof(TUser).EnsureCreate<TUser>();

                    var userId = await _identifierGenerator.GenerateUserIdAsync().ConfigureAndResultAsync();
                    await user.SetIdAsync(userId).ConfigureAndResultAsync();

                    var result = await _userManager.CreateUserByEmail<TUser, TCreatedBy>(_userStore,
                        _clock, user, model.Email, model.Password).ConfigureAndResultAsync();

                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";
                        model.UserId = userId.ToString();

                        // 确认邮件
                        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAndResultAsync();

                        var confirmEmailLocator = model.ConfirmEmailUrl.AsUriCombinerCore();
                        confirmEmailLocator.ChangeQueries(queries =>
                        {
                            queries.AddOrUpdate("userId", model.UserId, (key, value) => model.UserId);
                            queries.AddOrUpdate("code", code, (key, value) => code);
                        });
                        var confirmEmailExternalLink = HtmlEncoder.Default.Encode(confirmEmailLocator.ToString());

                        await _emailService.SendAsync(model.Email,
                            _localizer.GetString(r => r.ConfirmYourEmail)?.Value,
                            _localizer.GetString(r => r.ConfirmYourEmailFormat, confirmEmailExternalLink)?.Value).ConfigureAndWaitAsync();
                        //await userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default);

                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                        _logger.LogInformation(3, "User created a new account with password.");
                    }

                    IEnumerable<ContentError> errors = result.Errors;
                    if (errors.IsNotEmpty())
                    {
                        model.Errors.AddRange(errors.Select(error =>
                        {
                            return new Exception($"Code: {error.Code}, Description: {error.Description}");
                        }));
                    }

                    return model.Log(_logger);
                }
            );
        }

    }
}
