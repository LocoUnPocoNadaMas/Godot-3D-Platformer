extends Node

var _score: int = 0
var _currentScore: int = 0

var _currentScene: Node
var _currentLvl: String

# Called when the node enters the scene tree for the first time.
func _ready():
	printerr("inicio")
	LoadingScene("level_1.tscn")
	EventBus.UpdateMainScore.connect(CoinOnGiveMeYourMoney)
	EventBus.RestartEventHandler.connect(Restart)
	#EventBus.ChangeLevelEventHandler.connect(ChangeLevel)

func CoinOnGiveMeYourMoney(amount: int):
	_currentScore += amount
	printerr(_currentScore)

func Restart():
	#// Rollback score
	_currentScore = _score;
	#// Restart Scene
	GotoScene(_currentLvl);
	#// Update the Score Label
	EventBus.UpdateLabelScore.emit(_currentScore)
	
func GotoScene(path):
	_score = _currentScore
	# This function will usually be called from a signal callback,
	# or some other function in the current scene.
	# Deleting the current scene at this point is
	# a bad idea, because it may still be executing code.
	# This will result in a crash or unexpected behavior.

	# The solution is to defer the load to a later time, when
	# we can be sure that no code from the current scene is running:

	call_deferred("_deferred_goto_scene", path)


func _deferred_goto_scene(path):
	# It is now safe to remove the current scene
	_currentScene.free()
	LoadingScene(path)

func LoadingScene(path: String):
	_currentLvl = path;
	# Load the new scene.
	var s = ResourceLoader.load("res://scenes/"+path)

	# Instance the new scene.
	_currentScene = s.instantiate()
	
	var levelNode = get_node("Level")
	if levelNode.get_child_count() > 0:
		levelNode.remove_child(levelNode.get_child(0))
	levelNode.add_child(_currentScene)
