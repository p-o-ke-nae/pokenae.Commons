using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokenae.Commons.Filters
{
    /// <summary>
    /// APIアクセス権限をチェックするフィルタのインターフェース
    /// </summary>
    public interface IApiAccessFilter : IAsyncActionFilter
    {
    }
}
