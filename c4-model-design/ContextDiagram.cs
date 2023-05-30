using Structurizr;
//un comentario 

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem WorkFlowPro { get; private set; } //WorkFlowPro
		public SoftwareSystem GoogleCalendar { get; private set; } //Google Calendar: cronogramas
		public SoftwareSystem GoogleMeet { get; private set; } //Google Meet: reuniones
		public SoftwareSystem GoogleDrive {get; private set; } //Google Drive: guardar proyectos
		public SoftwareSystem EmailSystem {get; private set; }//Email system
		public SoftwareSystem Sunat {get; private set; }//Sunat: RUC de la empresa para verificar
		public SoftwareSystem Reniec {get; private set; }//Reniec: Verificar datos
		public Person Empleado { get; private set; } //Empleado
		public Person Jefe { get; private set; } //Jefe
		

		public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddElements();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddElements() {
			AddPeople();
			AddSoftwareSystems();
		}

		private void AddPeople()
		{
			Empleado = c4.Model.AddPerson("Empleado", "Ciudadano peruano.");
			Jefe = c4.Model.AddPerson("Jefe", "User Admin.");
		}

		private void AddSoftwareSystems()
		{
			WorkFlowPro = c4.Model.AddSoftwareSystem("Monitoreo del Traslado Aéreo de Vacunas SARS-CoV-2", "Permite el seguimiento y monitoreo del traslado aéreo a nuestro país de las vacunas para la COVID-19.");
			GoogleCalendar = c4.Model.AddSoftwareSystem("GoogleCalendar", "Plataforma que ofrece una REST API de información geo referencial.");
			GoogleMeet = c4.Model.AddSoftwareSystem("GoogleMeet", "Permite transmitir información en tiempo real por el avión del vuelo a nuestro sistema");
			GoogleDrive = c4.Model.AddSoftwareSystem("Google Drive"," ");
			EmailSystem = c4.Model.AddSoftwareSystem("EmailSystem ", " ");
			Sunat = c4.Model.AddSoftwareSystem("Sunat ", " ");
			Reniec = c4.Model.AddSoftwareSystem("Reniec", " ");
		}

		private void AddRelationships() {
			Empleado.Uses(WorkFlowPro, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");
			Jefe.Uses(WorkFlowPro, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");

			WorkFlowPro.Uses(GoogleMeet, "Consulta información en tiempo real por el avión del vuelo");
			WorkFlowPro.Uses(GoogleCalendar, "Usa la API de google maps");
			WorkFlowPro.Uses(GoogleDrive, " ");
			WorkFlowPro.Uses(EmailSystem, " ");
			WorkFlowPro.Uses(Sunat, " ");
			WorkFlowPro.Uses(Reniec, " ");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(Empleado)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
			styles.Add(new ElementStyle(nameof(Jefe)) { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });

			styles.Add(new ElementStyle(nameof(WorkFlowPro)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleCalendar)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleMeet)) { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(EmailSystem)){Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(GoogleDrive)){Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(Sunat)){Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(Reniec)){Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox});
		}

		private void SetTags()
		{
			Empleado.AddTags(nameof(Empleado));
			Jefe.AddTags(nameof(Jefe));

			WorkFlowPro.AddTags(nameof(WorkFlowPro));
			GoogleCalendar.AddTags(nameof(GoogleCalendar));
			GoogleMeet.AddTags(nameof(GoogleMeet));
			GoogleDrive.AddTags(nameof(GoogleDrive));
			EmailSystem.AddTags(nameof(EmailSystem));
			Sunat.AddTags(nameof(Sunat));
			Reniec.AddTags(nameof(Reniec));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(WorkFlowPro, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}