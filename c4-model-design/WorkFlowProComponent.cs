using Structurizr;

namespace c4_model_design
{
	public class WorkFlowProComponent
    {
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
        public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

        public WorkFlowProComponent(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
			this.containerDiagram = containerDiagram;
        }

		public void Generate() {
			AddComponents();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddComponents()
		{
            DomainLayer = containerDiagram.SextoBC.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            InterfaceLayer = containerDiagram.SextoBC.AddComponent("Interface Layer", "", "NodeJS (NestJS)");
            ApplicationLayer = containerDiagram.SextoBC.AddComponent("Application Layer", "", "NodeJS (NestJS)");
            InfrastructureLayer = containerDiagram.SextoBC.AddComponent("Infrastructure Layer", "", "NodeJS (NestJS)");
        }

        private void AddRelationships() {
            containerDiagram.ApiRest.Uses(InterfaceLayer, "", "");
            InterfaceLayer.Uses(ApplicationLayer, "", "");
            ApplicationLayer.Uses(DomainLayer, "", "");
            ApplicationLayer.Uses(InfrastructureLayer, "", "");
            InfrastructureLayer.Uses(DomainLayer, "", "");
            InfrastructureLayer.Uses(containerDiagram.Database, "Usa", "");
            InfrastructureLayer.Uses(contextDiagram.GoogleCalendar, "", "JSON/HTTPS");
            InfrastructureLayer.Uses(contextDiagram.GoogleMeet, "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();
		}

		private void SetTags()
		{
            DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            ApplicationLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
        }

		private void CreateView() {
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.SextoBC, "Monitoring Component Diagram", "Monitoring Component Diagram");
			componentView.Add(containerDiagram.MobileApplication);
			componentView.Add(containerDiagram.WebApplication);
			componentView.Add(containerDiagram.ApiRest);
			componentView.Add(containerDiagram.Database);
			componentView.Add(contextDiagram.GoogleMeet);
			componentView.Add(contextDiagram.GoogleCalendar);
			componentView.AddAllComponents();
		}
	}
}