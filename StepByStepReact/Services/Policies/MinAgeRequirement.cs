using Microsoft.AspNetCore.Authorization;

namespace StepByStepReact.Services.Policies
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public MinAgeRequirement(int age)
        {
            Age = age;
        }

        public int Age { get; private set; }
    }
}