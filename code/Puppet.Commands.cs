using System.Linq;

namespace Puppets
{
	public partial class Puppet : AnimatedEntity
	{
		[ConCmd.Server( "spawn_puppet" )]
		public static void SpawnPuppet( string username = "Anonymous", bool looksAtYou = true, int clothing = 0 )
		{
			if ( ConsoleSystem.Caller.Pawn is not AnimatedEntity player ) return;

			var spawnedPuppet = new Puppet();
			spawnedPuppet.Position = player.Position;
			spawnedPuppet.Rotation = player.Rotation;
			spawnedPuppet.Username = username;
			spawnedPuppet.LooksAtYou = looksAtYou;

			spawnedPuppet.Dress( clothing );
		}

		[ConCmd.Server( "spawn_clone" )]
		public static void SpawnClone( string username = "default", bool looksAtYou = true )
		{
			if ( ConsoleSystem.Caller.Pawn is not AnimatedEntity player ) return;

			var spawnedPuppet = new Puppet();
			spawnedPuppet.Position = player.Position;
			spawnedPuppet.Rotation = player.Rotation;
			spawnedPuppet.Username = username == "default" ? player.Client.Name : username;
			spawnedPuppet.LooksAtYou = looksAtYou;

			spawnedPuppet.Dress( player );
		}

		[ConCmd.Server( "toggle_speech" )]
		public static void ToggleSpeech( int targetId = 0 )
		{
			var puppet = Entity.All.OfType<Puppet>().Where( x => x.Identifier == targetId ).FirstOrDefault();

			if ( puppet != null )
				puppet.IsTalking = !puppet.IsTalking;
		}

		[ConCmd.Server( "undo_puppet" )]
		public static void UndoPuppet()
		{
			var target = Entity.All.OfType<Puppet>().OrderBy( x => x.Identifier ).LastOrDefault();
			if ( target != null )
				target.Delete();
		}
	}
}
