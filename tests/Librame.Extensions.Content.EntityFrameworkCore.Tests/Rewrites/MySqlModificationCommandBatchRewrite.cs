#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Pomelo.EntityFrameworkCore.MySql.Update.Internal
{
    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class MySqlModificationCommandBatchRewrite : MySqlModificationCommandBatch
    {
        public MySqlModificationCommandBatchRewrite
            (ModificationCommandBatchFactoryDependencies dependencies,
            int? maxBatchSize)
            : base(dependencies, maxBatchSize)
        {
        }


        /// <summary>
        ///     Generates a <see cref="RawSqlCommand" /> for the batch.
        /// </summary>
        /// <returns> The command. </returns>
        protected override RawSqlCommand CreateStoreCommand()
        {
            var commandBuilder = Dependencies.CommandBuilderFactory
                .Create()
                .Append(GetCommandText());

            var parameterValues = new Dictionary<string, object>(GetParameterCount());

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var commandIndex = 0; commandIndex < ModificationCommands.Count; commandIndex++)
            {
                var command = ModificationCommands[commandIndex];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var columnIndex = 0; columnIndex < command.ColumnModifications.Count; columnIndex++)
                {
                    var columnModification = command.ColumnModifications[columnIndex];
                    if (columnModification.ParameterName == "p255")
                    {
                        var name = columnModification.ParameterName;
                    }

                    if (columnModification.UseCurrentValueParameter)
                    {
                        commandBuilder.AddParameter(
                            columnModification.ParameterName,
                            Dependencies.SqlGenerationHelper.GenerateParameterName(columnModification.ParameterName),
                            columnModification.Property);

                        parameterValues.Add(columnModification.ParameterName, columnModification.Value);
                    }

                    if (columnModification.UseOriginalValueParameter)
                    {
                        commandBuilder.AddParameter(
                            columnModification.OriginalParameterName,
                            Dependencies.SqlGenerationHelper.GenerateParameterName(columnModification.OriginalParameterName),
                            columnModification.Property);

                        parameterValues.Add(columnModification.OriginalParameterName, columnModification.OriginalValue);
                    }
                }
            }

            return new RawSqlCommand(commandBuilder.Build(), parameterValues);
        }

    }
}
