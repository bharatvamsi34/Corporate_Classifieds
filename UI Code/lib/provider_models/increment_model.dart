import 'package:flutter/foundation.dart';

class Increment extends ChangeNotifier {
  int _counter = 0;
  int get counter => _counter;
  set counter(int val) {
    _counter = val;
    notifyListeners();
  }
  void incrementor(){
    counter++;
  }
  void decrement(){
    counter--;
  }
}