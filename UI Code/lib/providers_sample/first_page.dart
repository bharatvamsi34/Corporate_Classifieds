import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:sample_provider/provider_models/increment_model.dart';
import 'package:sample_provider/providers_sample/second_page.dart';

class FirstPage extends StatefulWidget {
  @override
  _FirstPageState createState() => _FirstPageState();
}

class _FirstPageState extends State<FirstPage> {
  @override
  Widget build(BuildContext context) {
    var count = Provider.of<Increment>(context);
    return Scaffold(
      appBar: AppBar(
        title: Row(children: <Widget>[
          Text('Provider demo'),
          Spacer(),
          IconButton(
              icon: Icon(Icons.navigate_next),
              onPressed: () {
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => SecondPage()),
                );
              }),
        ]),
      ),
      body: Center(
        child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Text(
                'You have pushed the button this many times:',
              ),
              Text('${count.counter}'),
            ]),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => count.incrementor(),
        child: Icon(Icons.add),
      ),
    );
  }
}
