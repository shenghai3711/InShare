﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Common
{
    public class Pager
    {
        /// <summary>
        /// 每一页数据的条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数据的条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 显示出来的页码的最多个数
        /// </summary>
        public int MaxPagerCount { get; set; }

        /// <summary>
        /// 当前页的页码的样式名字
        /// </summary>
        public string CurrentPageClassName { get; set; }

        /// <summary>
        /// 当前页的页码(从1开始算起始页)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 链接的格式，约定其中页码用{page}占位符
        /// </summary>
        public string UrlPattern { get; set; }
        /// <summary>
        /// 实现分页
        /// </summary>
        /// <returns></returns>
        public string GetPagerHtml()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<ul>");

            //ToDO：加上上一页、下一页、首页、末页、页面跳转等。

            int pageCount = (int)Math.Ceiling(TotalCount * 1.0 / PageSize);//总页数
            int startPageIndex = Math.Max(1, PageIndex - MaxPagerCount / 2);//显示出来的页码的起始页码
            int endPageIndex = Math.Min(pageCount, startPageIndex + MaxPagerCount);//显示出来的页码的结束页码
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                //是当前页
                if (i == PageIndex)
                {
                    html.Append("<li class='").Append(CurrentPageClassName).Append("'>")
                        .Append(i).Append("</li>");
                }
                else
                {
                    string href = UrlPattern.Replace("{pn}", i.ToString());
                    html.Append("<li><a href='").Append(href).Append("'>")
                        .Append(i).Append("</a></li>");
                }
            }
            html.Append("</ul>");
            return html.ToString();
        }
    }
}
