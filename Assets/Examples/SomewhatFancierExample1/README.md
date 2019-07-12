This example shows the progress of a fictious Puma proressing trough a world.

The `PumaProgress` is used to calculate a position and to trigger animations along the way. The value can be changed in the inspector.

The animations `ProgressProp` convert the progress to a target scale and only start a new animation when the in range boolean value changes (using `DistinctUntilChanged`).