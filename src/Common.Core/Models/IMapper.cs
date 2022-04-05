namespace Common.Core.Models;

public interface IMapper<out TOut>
{
    TOut Map();
}