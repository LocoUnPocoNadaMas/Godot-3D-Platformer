extends Area3D

func OnBodyEntered(body):
	if body.is_in_group(""):
		pass
	body.GameOver()
