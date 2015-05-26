using Autofac;
using Caliburn.Micro.Autofac;
using TouristClub.Data;
using TouristClub.Logic.Interface;
using TouristClub.Logic.Realisation;
using TouristClub.UI.ViewModels;

namespace TouristClub.UI
{
    class AppBotstrapper : AutofacBootstrapper<MainViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.Register(a => new DataContext("Data Source=PEGU7-PC;Initial Catalog=TouristClubDatabase;Integrated Security=True")).As<DataContext>().SingleInstance();
            builder.RegisterType<CampaignCrudService>().As<ICampaignCrudService>().SingleInstance();
            builder.RegisterType<CampaignTypeCrudService>().As<ICampaignTypeCrudService>().SingleInstance();
            builder.RegisterType<CategoryCrudService>().As<ICategoryCrudService>().SingleInstance();
            builder.RegisterType<CompetitionCrudService>().As<ICompetitionCrudService>().SingleInstance();
            builder.RegisterType<DiaryCrudService>().As<IDiaryCrudService>().SingleInstance();
            builder.RegisterType<GroupCrudService>().As<IGroupCrudService>().SingleInstance();
            builder.RegisterType<HeadCrudService>().As<IHeadCrudService>().SingleInstance();
            builder.RegisterType<PersonalDataCrudService>().As<IPersonalDataCrudService>().SingleInstance();
            builder.RegisterType<RoutePointCrudService>().As<IRoutePointCrudService>().SingleInstance();
            builder.RegisterType<SectionCrudService>().As<ISectionCrudService>().SingleInstance();
            builder.RegisterType<SportsmanCrudService>().As<ISportsmanCrudService>().SingleInstance();
            builder.RegisterType<StopCrudService>().As<IStopCrudService>().SingleInstance();
            builder.RegisterType<TouristCrudService>().As<ITouristCrudService>().SingleInstance();
            builder.RegisterType<TrainerCrudService>().As<ITrainerCrudService>().SingleInstance();
            builder.RegisterType<TrainigCrudService>().As<ITrainigCrudService>().SingleInstance();
        }
    }
}