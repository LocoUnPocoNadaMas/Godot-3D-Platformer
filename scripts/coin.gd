extends Area3D

@export var _spinSpeed: float = 2.0
@export var _bobHeight: float = 0.2
@export var _bobSpeed: float = 5.0
@export var _time: float = 0.0

@onready var _startY: float
	
signal GiveMeYourMoneyEventHandler(amount: int)

# Called when the node enters the scene tree for the first time.
func _ready():
	_startY = global_position.y

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float):
	#// Rotate
	rotate(Vector3.UP, _spinSpeed * delta)
	#// Up and down
	_time += delta
	var distance = (sin(_time * _bobSpeed) + 1) / 2
	global_position.y = _startY + (distance * _bobHeight)

func OnBodyEntered(body: Node3D):
	#if (body is not Player bodyInstance) return;
	#//bodyInstance.AddScore(1);
	if body.is_in_group(""):
		pass
	EventBus.UpdateScoreEventHandler.emit(1)
	queue_free()
