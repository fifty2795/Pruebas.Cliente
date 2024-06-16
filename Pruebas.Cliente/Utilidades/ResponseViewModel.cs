using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Utilidades
{
    public class ResponseViewModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public PaginatedList<T?> DataList { get; set; }

        public ErrorViewModel? Error { get; set; }
    }

    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorViewModel? Error { get; set; }
    }
}
