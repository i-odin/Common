using System.Collections;

namespace Common.Core.QueryBuilders;

public class Parameter
{
    public Parameter(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }

    public object Value { get; set; }
}

public class Parameters : IEnumerable<KeyValuePair<string, object>>
{
    private readonly List<Parameter> _parameters = new List<Parameter>();

    public Parameters Add(string name, object value)
    {
        _parameters.Add(new Parameter(name, value));
        return this;
    }

    public Parameters Add(Parameter parameter)
    {
        _parameters.Add(parameter);
        return this;
    }

    public void Add(Parameters parameters)
    {
        if (parameters != null && parameters._parameters.Any())
        {
            _parameters.AddRange(parameters._parameters);
        }
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        foreach (var spryParameter in _parameters)
        {
            yield return new KeyValuePair<string, object>(spryParameter.Name, spryParameter.Value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
