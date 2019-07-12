This example shows the setup of the worlds weirdest coffee bar. Random coffee orders are generated at intervals or at manual request.

The total cost of all emitted orders is caluclated dynamically using the `Scan` operator. And it combines multiple input streams (Intervals and a manual subject) to generate a healty coffee order stream.

Using the `OfType<TSource, TOutput>` it filters on the only correct coffee choice and show a little effect when that is orderd.

A very important thing to get your head around is the `Share` operator on the intervals in `SomewhatFancierExample3`. Without it each subscribtion would initiate a new, fresh set of intervals, startting at the moment of subscription. Even worse, since we're selecting random coffees from the interval, without it each subscriber would get a different stream of coffees.
