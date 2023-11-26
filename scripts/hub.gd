extends Control

var _lblScore: Label

# Called when the node enters the scene tree for the first time.
func _ready():
	_lblScore = get_node("lblScore")
	EventBus.UpdateLabelScore.connect(OnUpdateLabelScore)
	
func OnUpdateLabelScore(amount: int):
	_lblScore.text= str("Score: ", amount)
