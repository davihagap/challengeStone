namespace api.Domain.Models
{
    public interface ITaxavel
    {
        ITransacao Taxa {get; set;}
        ITransacao calculaTaxa();
    }
}