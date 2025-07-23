import 'package:flutter/material.dart';
import 'screens/login_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'VBT Login/Register',
      theme: ThemeData(primarySwatch: Colors.green),
      home: const LoginScreen(),
      debugShowCheckedModeBanner: false,
    );
  }
}
