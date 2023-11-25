extends Node

var _score: int = 0

var _currentLevel: PackedScene

# Called when the node enters the scene tree for the first time.
func _ready():
	printerr("inicio")
	ChangeLevel("level_1.tscn")
	EventBus.UpdateScoreEventHandler.connect(CoinOnGiveMeYourMoney)
	EventBus.ChangeLevelEventHandler.connect(ChangeLevel)
	EventBus.RestartEventHandler.connect(Restart)

func Restart():
	printerr("restart")
	var levelNode = get_node("Level")
	levelNode.get_tree().reload_current_scene()

func ChangeLevel(level: String):
	_currentLevel = load("res://scenes/"+level)
	var newLevelIns = _currentLevel.instantiate()
	var levelNode = get_node("Level")
	if levelNode.get_child_count() > 0:
		levelNode.remove_child(levelNode.get_child(0))
	levelNode.add_child(newLevelIns)

func CoinOnGiveMeYourMoney(amount: int):
	_score += amount
	printerr(_score)
