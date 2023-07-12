using Practical20.Repository;

namespace Practical20.Models
{
    public class User: IAuditable
    {
        public int Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

        public string Password { get; set; }
    }
}
