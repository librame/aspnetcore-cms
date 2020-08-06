#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Mappers
{
    using Core.Mappers;
    using Data.Mappers;

    /// <summary>
    /// 内容访问器类型参数映射器。
    /// </summary>
    public class ContentAccessorTypeParameterMapper : AbstractTypeParameterMapper
    {
        /// <summary>
        /// 构造一个 <see cref="ContentAccessorTypeParameterMapper"/>。
        /// </summary>
        /// <param name="baseMapper">给定的基础访问器 <see cref="AccessorTypeParameterMapper"/>。</param>
        /// <param name="mappings">给定的 <see cref="TypeParameterMappingCollection"/>。</param>
        public ContentAccessorTypeParameterMapper(AccessorTypeParameterMapper baseMapper,
            TypeParameterMappingCollection mappings)
            : base(mappings)
        {
            BaseMapper = baseMapper.NotNull(nameof(baseMapper));
        }


        /// <summary>
        /// 基础访问器类型参数映射器。
        /// </summary>
        /// <value>返回 <see cref="AccessorTypeParameterMapper"/>。</value>
        public AccessorTypeParameterMapper BaseMapper { get; }


        /// <summary>
        /// 分类映射。
        /// </summary>
        public TypeParameterMapping Category
            => GetGenericMapping(nameof(Category));

        /// <summary>
        /// 来源映射。
        /// </summary>
        public TypeParameterMapping Source
            => GetGenericMapping(nameof(Source));

        /// <summary>
        /// 声明映射。
        /// </summary>
        public TypeParameterMapping Claim
            => GetGenericMapping(nameof(Claim));

        /// <summary>
        /// 标签映射。
        /// </summary>
        public TypeParameterMapping Tag
            => GetMapping($"T{nameof(Tag)}");

        /// <summary>
        /// 单元映射。
        /// </summary>
        public TypeParameterMapping Unit
            => GetGenericMapping(nameof(Unit));

        /// <summary>
        /// 单元声明映射。
        /// </summary>
        public TypeParameterMapping UnitClaim
            => GetGenericMapping(nameof(UnitClaim));

        /// <summary>
        /// 单元标签映射。
        /// </summary>
        public TypeParameterMapping UnitTag
            => GetGenericMapping(nameof(UnitTag));

        /// <summary>
        /// 单元访问计数映射。
        /// </summary>
        public TypeParameterMapping UnitVisitCount
            => GetGenericMapping(nameof(UnitVisitCount));

        /// <summary>
        /// 窗格映射。
        /// </summary>
        public TypeParameterMapping Pane
            => GetGenericMapping(nameof(Pane));

        /// <summary>
        /// 窗格单元映射。
        /// </summary>
        public TypeParameterMapping PaneUnit
            => GetGenericMapping(nameof(PaneUnit));
    }
}
