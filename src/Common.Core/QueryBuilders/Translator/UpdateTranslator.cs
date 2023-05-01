﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IUpdateTranslator<T>
        where T : class
{
    IUpdateTranslator<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
}

public class UpdateTranslator<T> : Translator<T>, IUpdateTranslator<T>
    where T : class
{
    private bool _isComma;
    public UpdateTranslator(StringBuilder sb) : base(sb) { }

    public UpdateTranslator<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        if (_isComma) Comma();
        else _isComma = true;

        Field(field).Equal();
        if (value is null)
            AppendNull();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }
    IUpdateTranslator<T> IUpdateTranslator<T>.Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        Set(field, value); return this;
    }

    private UpdateTranslator<T> Update()
    {
        AppendNewLine("update ");
        _sb.Append(typeEntity.Name);
        AppendNewLine("set ");
        return this;
    }

    public static implicit operator UpdateTranslator<T>(StringBuilder sb)
        => new UpdateTranslator<T>(sb).Update();
}