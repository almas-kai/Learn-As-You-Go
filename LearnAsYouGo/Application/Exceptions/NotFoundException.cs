namespace Application.Exceptions;

public class NotFoundException(string massage) : Exception(massage)
{
}
