
public class RoomManager : Singleton<RoomManager>
{
	public string RoomName { get; set; } = "";
	public string RoomType { get; set; } = "PUBLIC";
	public string RoomPassword { get; set; } = "";
	public byte MaxPlayers { get; } = 4;
}
