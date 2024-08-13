namespace book_lending.DTO;

public class CreateUserRequest
{
    public CreateUserRequest(string login, string password, string repeatPassword)
    {
        Login = login;
        Password = password;
        RepeatPassword = repeatPassword;
    }

    public string Login { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}