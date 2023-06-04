using Structurizr;

namespace c4_model_design
{
	public class ContainerDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		public Container MobileApplication { get; private set; } // aplicacion mobilo
		public Container WebApplication { get; private set; } //pagina web aplicacion
		public Container LandingPage { get; private set; } // nuestra landing page
		public Container ApiRest { get; private set; } // nuestra API

		//bounded contexts
		public Container AcountsBC { get; private set; } //Acount context
		public Container ResponsibilityBC { get; private set; } //Responsibility context
		public Container PaymentsBC { get; private set; } // Payments context
		public Container GroupBC { get; private set; } // Group Context
		public Container SecurityBC { get; private set; } // Security COntext
		public Container CommunicationBC { get; private set; } // Communication context
		public Container ServiceBC { get; private set; }//Service context
		public Container NotificationBC { get; private set; }//Notification context


		public Container Database { get; private set; } //nuestra base de datos

		public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
		}

		public void Generate() {
			AddContainers();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddContainers()
		{
			MobileApplication = contextDiagram.WorkFlowPro.AddContainer("Mobile App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "Swift UI");
			WebApplication = contextDiagram.WorkFlowPro.AddContainer("Web App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "React");
			LandingPage = contextDiagram.WorkFlowPro.AddContainer("Landing Page", "", "React");

			ApiRest = contextDiagram.WorkFlowPro.AddContainer("API REST", "API REST", "NodeJS (NestJS) port 8080");

            AcountsBC = contextDiagram.WorkFlowPro.AddContainer("AcountsBC", "Bounded context que se encarga de la gestion de los perfiles de los usuarios ", "NodeJS (NestJS)");
            ResponsibilityBC = contextDiagram.WorkFlowPro.AddContainer("ResponsibilityBC", "Bounded context que se encarga de la gestion de las actividades que realiza el Empleado  ", "NodeJS (NestJS)");
            PaymentsBC= contextDiagram.WorkFlowPro.AddContainer("PaymentsBC", "Bounded context que se encarga los pagos de las suscripciones de las empresas por el servicio WorkFlowPro ", "NodeJS (NestJS)");
            GroupBC= contextDiagram.WorkFlowPro.AddContainer("GroupBC","Bounded context que se encarga de la creación de grupos de trabajos por los altos mandos ", "NodeJS (NestJS)");
            SecurityBC = contextDiagram.WorkFlowPro.AddContainer("SecurityBC", "Bounded context que se encarga de la verificacion de los datos de los usuarios ", "NodeJS (NestJS)");
            CommunicationBC = contextDiagram.WorkFlowPro.AddContainer("CommunicationBC", "Bounded context que se encarga de la interaccion entre los usuarios ", "NodeJS (NestJS)");
			ServiceBC = contextDiagram.WorkFlowPro.AddContainer("ServiceBC", "Bounded context que se encarga de brindar servicios de ayuda en las actividades de los usuarios", "NodeJS (NestJS)");
			NotificationBC = contextDiagram.WorkFlowPro.AddContainer("NotificationBC", "Bounded context que se encarga de comunicar las actividades entre los altos mandos y los empleados", "NodeJS (NestJS)");

			Database = contextDiagram.WorkFlowPro.AddContainer("DB", "", "MySQL Server RDS AWS");
		}

		private void AddRelationships() {
			contextDiagram.Empleado.Uses(MobileApplication, "Consulta");
			contextDiagram.Empleado.Uses(WebApplication, "Consulta");
			contextDiagram.Empleado.Uses(LandingPage, "Consulta");

			contextDiagram.Jefe.Uses(MobileApplication, "Consulta");
			contextDiagram.Jefe.Uses(WebApplication, "Consulta");
			contextDiagram.Jefe.Uses(LandingPage, "Consulta");

			MobileApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");
			WebApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");

            ApiRest.Uses(AcountsBC, "API Request", "JSON/HTTPS");
            ApiRest.Uses(ResponsibilityBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(PaymentsBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(GroupBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(SecurityBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(CommunicationBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(ServiceBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(NotificationBC, "API Request", "JSON/HTTPS");

            AcountsBC.Uses(Database, "", "");
            ResponsibilityBC.Uses(Database, "", "");
            PaymentsBC.Uses(Database, "", "");
            GroupBC.Uses(Database, "", "");
            SecurityBC.Uses(Database, "", "");
            CommunicationBC.Uses(Database, "", "");
			ServiceBC.Uses(Database, "", "");
			NotificationBC.Uses(Database, "", "");

			AcountsBC.Uses(contextDiagram.Reniec, "API Request", "JSON/HTTPS");
			AcountsBC.Uses(contextDiagram.Sunat, "API Request", "JSON/HTTPS");
			AcountsBC.Uses(contextDiagram.EmailSystem, "API Request", "JSON/HTTPS");
			ResponsibilityBC.Uses(contextDiagram.GoogleDrive, "API Request", "JSON/HTTPS");
			PaymentsBC.Uses(contextDiagram.Stripe, "API Request", "JSON/HTTPS");
			SecurityBC.Uses(contextDiagram.EmailSystem, "API Request", "JSON/HTTPS");
			SecurityBC.Uses(contextDiagram.Reniec, "API Request", "JSON/HTTPS");
			CommunicationBC.Uses(contextDiagram.GoogleCalendar, "API Request", "JSON/HTTPS");
			CommunicationBC.Uses(contextDiagram.GoogleMeet, "API Request", "JSON/HTTPS");
			NotificationBC.Uses(contextDiagram.GoogleCalendar, "API Request", "JSON/HTTPS");
			NotificationBC.Uses(contextDiagram.EmailSystem, "API Request", "JSON/HTTPS");


            
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });

            styles.Add(new ElementStyle(nameof(AcountsBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle(nameof(ResponsibilityBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(PaymentsBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(GroupBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(SecurityBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(CommunicationBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(ServiceBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(NotificationBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
		}

		private void SetTags()
		{
			MobileApplication.AddTags(nameof(MobileApplication));
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));

            AcountsBC.AddTags(nameof(AcountsBC));
            ResponsibilityBC.AddTags(nameof(ResponsibilityBC));
            PaymentsBC.AddTags(nameof(PaymentsBC));
            GroupBC.AddTags(nameof(GroupBC));
            SecurityBC.AddTags(nameof(SecurityBC));
            CommunicationBC.AddTags(nameof(CommunicationBC));
			ServiceBC.AddTags(nameof(ServiceBC));
			NotificationBC.AddTags(nameof(NotificationBC));

			Database.AddTags(nameof(Database));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.WorkFlowPro, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}