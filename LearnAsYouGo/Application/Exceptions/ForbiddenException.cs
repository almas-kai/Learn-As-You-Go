namespace Application.Exceptions;

public class ForbiddenException(string massage) : Exception(massage)
{
}
