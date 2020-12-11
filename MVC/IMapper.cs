namespace MVC
{
    public interface IMapper<in TFrom, out TO>
    { 
        TO MapTo(TFrom from);
    }
}