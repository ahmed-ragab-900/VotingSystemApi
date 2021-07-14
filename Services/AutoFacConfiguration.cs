using Autofac;
using Microsoft.AspNetCore.Http;
using VotingSystemApi.Services.Account;
using VotingSystemApi.Services.Candidates;
using VotingSystemApi.Services.Commissions;
using VotingSystemApi.Services.Complaints;
using VotingSystemApi.Services.Elections;
using VotingSystemApi.Services.Posts;
using VotingSystemApi.Services.Response;
using VotingSystemApi.Services.StudentUnion;
using VotingSystemApi.Services.Users;
using VotingSystemApi.Services.Voting;

namespace VotingSystemApi.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<AccountServices>().As<IAccountServices>().InstancePerLifetimeScope();
            builder.RegisterType<UserServices>().As<IUserServices>().InstancePerLifetimeScope();
            builder.RegisterType<CandidateServices>().As<ICandidateServices>().InstancePerLifetimeScope();
            builder.RegisterType<ComplaintServices>().As<IComplaintServices>().InstancePerLifetimeScope();
            builder.RegisterType<CommissionServices>().As<ICommissionServices>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionServices>().As<IElectionServices>().InstancePerLifetimeScope();
            builder.RegisterType<PostServices>().As<IPostServices>().InstancePerLifetimeScope();
            builder.RegisterType<ResponseServices>().As<IResponseServices>().InstancePerLifetimeScope();
            builder.RegisterType<StudentUnionServices>().As<IStudentUnionServices>().InstancePerLifetimeScope();
            builder.RegisterType<VotingServices>().As<IVotingServices>().InstancePerLifetimeScope();

            //Validators
        }
    }
}
