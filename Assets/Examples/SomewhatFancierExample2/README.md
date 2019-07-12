This example shows a slow health bar and commands.

The health bars use a single health value as input. The slow part just uses `Throttle` to delay the animation until it is not changing for a short while.

The normalized health value is calculated using the reactive max health property, so when either value changes, the normalized values changes with it.

The commands use the same health source to determine their can execute state.

Note that you can also change the health value in the inspector directly and everything still updates accordingly.
