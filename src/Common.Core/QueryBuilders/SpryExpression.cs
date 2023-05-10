﻿using System.Linq.Expressions;

namespace Common.Core.QueryBuilders;

internal static class CommonExpression
{
    internal static string GetColumnName<TDtoType, TProperty>(Expression<Func<TDtoType, TProperty>> columnExpression)
    {
        var memberExpression = columnExpression.Body as MemberExpression;

        if (memberExpression == null)
        {
            var contantExpression = columnExpression.Body as ConstantExpression;

            if (contantExpression == null)
                throw new ArgumentException("Invalid Column Expression");

            return Convert.ToString(contantExpression.Value);
        }

        return memberExpression.Member.Name;
    }

    internal static object GetColumnValue<TDtoType, TProperty>(Expression<Func<TDtoType, TProperty>> columnExpression)
    {
        Expression memberExpression = columnExpression.Body as MemberExpression;

        if (memberExpression == null)
        {
            var contantExpression = columnExpression.Body as ConstantExpression;

            if (contantExpression == null)
                throw new ArgumentException("Invalid Column Expression");

            memberExpression = contantExpression;
        }

        var valueExpression = Expression.Convert(memberExpression, typeof(TProperty));

        var valueFunc = Expression.Lambda<Func<TProperty>>(valueExpression).Compile();

        return valueFunc();
    }
}