import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:sample_provider/provider_models/increment_model.dart';

class SecondPage extends StatefulWidget {
  @override
  _SecondPageState createState() => _SecondPageState();
}

class _SecondPageState extends State<SecondPage> {
  @override
  Widget build(BuildContext context) {
    var count = Provider.of<Increment>(context);
    return Scaffold(
      body: Container(
          padding: EdgeInsets.all(100),
          color: Colors.white,
          child: Center(
            child: Column(children: <Widget>[
              Text('counter : ${count.counter}'),
              Spacer(),
              IconButton(
                icon: Icon(Icons.add),
                onPressed: () => count.incrementor(),
              ),
              IconButton(
                icon: Icon(Icons.remove),
                onPressed: () => count.decrement(),
              ),
              IconButton(
                icon: Icon(Icons.arrow_back_ios),
                onPressed: () => Navigator.of(context).pop(),
              ),
            ]),
          )),
    );
  }
}
