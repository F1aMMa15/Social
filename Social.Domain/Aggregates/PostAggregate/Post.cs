using FluentValidation;
using Social.Domain.Aggregates.UserProfileAggregate;
using Social.Domain.Validators;

namespace Social.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private readonly List<PostComment> _comments = new List<PostComment>();
        private readonly List<PostInteraction> _interaction = new List<PostInteraction>();

        private Post() { }

        public Guid Id { get; private set; }
        public string TextContent { get; private set; } = null!;
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public IEnumerable<PostComment> Comments => _comments;
        public IEnumerable<PostInteraction> Interactions => _interaction;

        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; } = null!;

        public static Post CreatePost(string textContent, Guid userProfileId)
        {
            var post =  new Post
            {
                TextContent = textContent,
                UserProfileId = userProfileId,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };

            var validator = new PostValidator();
            validator.ValidateAndThrow(post);

            return post;
        }

        public void UpdateTextContent(string content)
        {
            TextContent = content;
            LastModifiedDate = DateTime.Now;
        }

        public void AddComment(PostComment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(PostComment comment)
        {
            _comments.Remove(comment);
        }

        public void AddInteraction(PostInteraction interaction)
        {
            _interaction.Add(interaction);
        }

        public void RemoveInteraction(PostInteraction interaction)
        {
            _interaction.Remove(interaction);
        }
    }
}
