namespace CODE_Frontend.Mappers
{
    public interface IMapper<in TFrom, out TO>
    { 
        TO MapTo(TFrom from);
    }
}