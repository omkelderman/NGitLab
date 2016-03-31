using NGitLab.Impl;
using NGitLab.Models;

namespace NGitLab
{
    public class GitLabClient
    {
        private GitLabClient(string hostUrl, string apiToken)
        {
            _api = new API(hostUrl, apiToken);
            Users = new UserClient(_api);
            Projects = new ProjectClient(_api);
            Issues = new IssueClient(_api);
            Groups = new NamespaceClient(_api);
        }

        public static GitLabClient Connect(string hostUrl, string apiToken)
        {
            return new GitLabClient(hostUrl, apiToken);
        }

        public static Session LoginWithUsername(string hostUrl, string username, string password)
        {
            return Login(hostUrl, new LoginRequest() {Login = username, Password = password});
        }

        public static Session LoginWithEmail(string hostUrl, string email, string password)
        {
            return Login(hostUrl, new LoginRequest() { Email = email, Password = password });
        }

        private static Session Login(string hostUrl, LoginRequest loginRequest)
        {
            var gitlabApi = new API(hostUrl, null);
            return gitlabApi.Post().With(loginRequest).To<Session>("/session");
        }

        private readonly API _api;

        public readonly IUserClient Users;
        public readonly IProjectClient Projects;
        public readonly IIssueClient Issues;
        public readonly INamespaceClient Groups;

        public IRepositoryClient GetRepository(int projectId)
        {
            return new RepositoryClient(_api, projectId);
        }

        public IMergeRequestClient GetMergeRequest(int projectId)
        {
            return new MergeRequestClient(_api, projectId);
        }
    }
}