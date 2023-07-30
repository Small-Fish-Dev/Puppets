using System.Linq;

namespace Puppets
{
	public partial class Puppet : AnimatedEntity
	{
		[Net] public string Username { get; private set; } = "Anonymous";
		public NameTag NameTag { get; private set; }
		public bool IsTalking { get; private set; } = false;
		public int Identifier { get; private set; } = 0;
		public bool LooksAtYou { get; private set; } = false;
		public Vector3 Target => Game.Clients.FirstOrDefault().Pawn?.Position ?? Rotation.Forward;

		public Puppet() { }
		
		public Puppet( string username )
		{
			Username = username;
		}

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen/citizen.vmdl" );

			Identifier = Entity.All.OfType<Puppet>().Count();
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();

			NameTag = new NameTag();
			NameTag.Position = Position + Vector3.Up * 76f;
		}

		public void Dress( int clothingId = 0 )
		{
			/*if ( clothingId == 0 )
				Clothes.Presets.Ran
			Clothes.Presets.*/
			new Clothes().Dress( this );
		}

		public void Dress( AnimatedEntity copy )
		{
			var clothingContainer = new ClothingContainer();

			foreach ( Entity child in copy.Children )
			{
				if ( child is not AnimatedEntity childModel ) return;

				Clothing clothing = new Clothing();
				clothing.Model = childModel.GetModelName();
				clothingContainer.Toggle( clothing );
			}

			clothingContainer.DressEntity( this );
		}

		[GameEvent.Tick.Server]
		public void ComputeActing()
		{
			if ( LooksAtYou )
			{
				var animationHelper = new CitizenAnimationHelper( this );
				animationHelper.WithLookAt( Target );
			}

			if ( Time.Tick % 4 == 0 )
				SetAnimParameter( "voice", IsTalking ? Game.Random.Float( 0, 1 ) : 0 );
		}

		[GameEvent.Client.Frame]
		public void MoveNameTag()
		{
			if ( NameTag != null )
			{
				if ( Username == "none" )
				{
					NameTag.Delete();
					return;
				}

				NameTag.Position = Position + Vector3.Up * 85f;
				NameTag.Rotation = Rotation.LookAt( Camera.Main.Position - NameTag.Position );
				NameTag.Label.Text = Username;
				var textSize = Username.Length * 150f;
				NameTag.PanelBounds = new Rect( 0, 0, textSize, 200 );
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			NameTag?.Delete();
		}
	}
}
