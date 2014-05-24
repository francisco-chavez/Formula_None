using System.Xml.Serialization;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleModel
		: IModel
	{
		#region Properties
		[XmlAttribute]
		public virtual double X		{ get; set; }

		[XmlAttribute]
		public virtual double Y		{ get; set; }
		#endregion


		#region Constructors
		public ObstacleModel() { }
		#endregion
	}
}
