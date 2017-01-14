public delegate void HitHandler();
public delegate void StandHandler();
public delegate void ActivateHandler(bool value);

public interface IParticipant 
{
	void MakeStep ();
	event HitHandler OnHit;
	event StandHandler OnStand;
	event ActivateHandler OnActivate;
}

