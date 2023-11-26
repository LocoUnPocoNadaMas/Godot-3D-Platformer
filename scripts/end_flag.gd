extends Area3D

func _on_body_entered(_body):
	EventBus.ChangeLevelEventHandler.emit("level_2.tscn")
