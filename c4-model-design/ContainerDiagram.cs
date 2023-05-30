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
		public Container PrimerBC { get; private set; }
		public Container SegundoBC { get; private set; }
		public Container TercerBC { get; private set; }
		public Container CuartoBC { get; private set; }
		public Container QuintoBC { get; private set; }
		public Container SextoBC { get; private set; }
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

            PrimerBC = contextDiagram.WorkFlowPro.AddContainer("PrimerBC", " ", "NodeJS (NestJS)");
            SegundoBC = contextDiagram.WorkFlowPro.AddContainer("SegundoBC", " ", "NodeJS (NestJS)");
            TercerBC = contextDiagram.WorkFlowPro.AddContainer("TercerBC", " ", "NodeJS (NestJS)");
            CuartoBC = contextDiagram.WorkFlowPro.AddContainer("CuartoBC"," ", "NodeJS (NestJS)");
            QuintoBC = contextDiagram.WorkFlowPro.AddContainer("QuintoBC", " ", "NodeJS (NestJS)");
            SextoBC = contextDiagram.WorkFlowPro.AddContainer("SextoBC", " ", "NodeJS (NestJS)");

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

            ApiRest.Uses(PrimerBC, "API Request", "JSON/HTTPS");
            ApiRest.Uses(SegundoBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(TercerBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(CuartoBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(QuintoBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(SextoBC, "API Request", "JSON/HTTPS");

            PrimerBC.Uses(Database, "", "");
            SegundoBC.Uses(Database, "", "");
            TercerBC.Uses(Database, "", "");
            CuartoBC.Uses(Database, "", "");
            QuintoBC.Uses(Database, "", "");
            SextoBC.Uses(Database, "", "");

            SextoBC.Uses(contextDiagram.GoogleCalendar, "API Request", "JSON/HTTPS");
            SextoBC.Uses(contextDiagram.GoogleMeet, "API Request", "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });

            styles.Add(new ElementStyle(nameof(PrimerBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle(nameof(SegundoBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(TercerBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(CuartoBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(QuintoBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(SextoBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
		}

		private void SetTags()
		{
			MobileApplication.AddTags(nameof(MobileApplication));
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));

            PrimerBC.AddTags(nameof(PrimerBC));
            SegundoBC.AddTags(nameof(SegundoBC));
            TercerBC.AddTags(nameof(TercerBC));
            CuartoBC.AddTags(nameof(CuartoBC));
            QuintoBC.AddTags(nameof(QuintoBC));
            SextoBC.AddTags(nameof(SextoBC));

			Database.AddTags(nameof(Database));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.WorkFlowPro, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}