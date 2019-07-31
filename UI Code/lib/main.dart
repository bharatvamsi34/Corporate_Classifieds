import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:sample_provider/provider_models/increment_model.dart';
import 'package:sample_provider/providers_sample/first_page.dart';

void main() {
  Provider.debugCheckInvalidValueType = null;
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider<Increment>.value(
          value: Increment(),
        )
      ],
      child: MaterialApp(
        title: 'sample provider',
        theme: ThemeData(
          primarySwatch: Colors.blue,
        ),
        home: FirstPage(),
      ),
    );
  }
}
