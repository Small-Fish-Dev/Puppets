using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Puppets
{
	public class NameTag : WorldPanel
	{
		public Label Label { get; set; }

		public NameTag()
		{
			StyleSheet.Load( "Style.scss" );
			Label = Add.Label( "Anonymous", "NameTag" );
		}
	}
}
