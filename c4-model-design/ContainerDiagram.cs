using Structurizr;

namespace c4_model_design
{
	public class ContainerDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		public Container MobileApplication { get; private set; }
		public Container WebApplication { get; private set; }
		public Container LandingPage { get; private set; }
		public Container ApiRest { get; private set; }
		public Container SecurityBC { get; private set; }
		public Container FlightPlanningBC { get; private set; }
		public Container AirportBC { get; private set; }
		public Container AircraftInventoryBC { get; private set; }
		public Container VaccinesInventoryBC { get; private set; }
		public Container MonitoringBC { get; private set; }
		public Container Database { get; private set; }

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

            SecurityBC = contextDiagram.WorkFlowPro.AddContainer("Security BC", "Seguridad", "NodeJS (NestJS)");
            FlightPlanningBC = contextDiagram.WorkFlowPro.AddContainer("Flight Planning BC", "Planificación de Vuelos", "NodeJS (NestJS)");
            AirportBC = contextDiagram.WorkFlowPro.AddContainer("Airport BC", "Información de Aeropuertos", "NodeJS (NestJS)");
            AircraftInventoryBC = contextDiagram.WorkFlowPro.AddContainer("Aircraft Inventory BC", "Inventario de Aviones", "NodeJS (NestJS)");
            VaccinesInventoryBC = contextDiagram.WorkFlowPro.AddContainer("Vaccines Inventory BC", "Inventario de Vacunas", "NodeJS (NestJS)");
            MonitoringBC = contextDiagram.WorkFlowPro.AddContainer("Monitoring BC", "Monitoreo en tiempo real del status y ubicación del vuelo que transporta las vacunas", "NodeJS (NestJS)");

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

            ApiRest.Uses(SecurityBC, "API Request", "JSON/HTTPS");
            ApiRest.Uses(FlightPlanningBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(AirportBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(AircraftInventoryBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(VaccinesInventoryBC, "API Request", "JSON/HTTPS");
			ApiRest.Uses(MonitoringBC, "API Request", "JSON/HTTPS");

            SecurityBC.Uses(Database, "", "");
            FlightPlanningBC.Uses(Database, "", "");
            AirportBC.Uses(Database, "", "");
            AircraftInventoryBC.Uses(Database, "", "");
            VaccinesInventoryBC.Uses(Database, "", "");
            MonitoringBC.Uses(Database, "", "");

            MonitoringBC.Uses(contextDiagram.GoogleCalendar, "API Request", "JSON/HTTPS");
            MonitoringBC.Uses(contextDiagram.GoogleMeet, "API Request", "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });

            styles.Add(new ElementStyle(nameof(SecurityBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle(nameof(AirportBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(AircraftInventoryBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(FlightPlanningBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(MonitoringBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(VaccinesInventoryBC)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
		}

		private void SetTags()
		{
			MobileApplication.AddTags(nameof(MobileApplication));
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));

            SecurityBC.AddTags(nameof(SecurityBC));
            AirportBC.AddTags(nameof(AirportBC));
            AircraftInventoryBC.AddTags(nameof(AircraftInventoryBC));
            FlightPlanningBC.AddTags(nameof(FlightPlanningBC));
            MonitoringBC.AddTags(nameof(MonitoringBC));
            VaccinesInventoryBC.AddTags(nameof(VaccinesInventoryBC));

			Database.AddTags(nameof(Database));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.WorkFlowPro, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}