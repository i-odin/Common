﻿using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IJoinTranslator<T>
    where T : class
{
}

public class JoinTranslator<T> : Translator<T>, IJoinTranslator<T>
    where T : class
{
    public JoinTranslator(StringBuilder sb) : base(sb) { }

    public static implicit operator JoinTranslator<T>(StringBuilder sb)
        => new JoinTranslator<T>(sb);
}
