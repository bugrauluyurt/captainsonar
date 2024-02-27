Notes

// Game => Game needs to be stored
Session: Session

// State
Position: {
	teamId1: {
		dots: Dot[]
	}
	teamId2: {
		dots: Dot[]
	}
}
Vessels: VesselBody[]
Assets: Asset[]
Status: GameStatus // Prestart, Continued, Over,
Victor; null | TeamName
Turn: {
	teamId: string,
	playerId: string,
}


public State generateState(Command[] commands)