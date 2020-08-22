#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Content.Api.Models
{
    /// <summary>
    /// 单元访问计数模型。
    /// </summary>
    public class UnitVisitCountModel
    {
        ///// <summary>
        ///// 单元模型。
        ///// </summary>
        //public UnitModel Unit { get; set; }

        /// <summary>
        /// 转发次数。
        /// </summary>
        public string RetweetCount { get; set; }

        /// <summary>
        /// 反对人数。
        /// </summary>
        public string ObjectorCount { get; set; }

        /// <summary>
        /// 支持人数。
        /// </summary>
        public string SupporterCount { get; set; }

        /// <summary>
        /// 收藏人数。
        /// </summary>
        public string FavoriteCount { get; set; }


        /// <summary>
        /// 访问次数。
        /// </summary>
        public string VisitCount { get; set; }

        /// <summary>
        /// 访问人数。
        /// </summary>
        public string VisitorCount { get; set; }
    }
}
