import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'register_screen.dart';
import 'home_screen.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  bool _obscure = true;
  bool _loading = false;

  void _login() async {
    setState(() => _loading = true);

    final email = Uri.encodeQueryComponent(_emailController.text);
    final password = Uri.encodeQueryComponent(_passwordController.text);

    final url = Uri.parse(
      'http://10.0.2.2:5248/auth/login?email=$email&password=$password',
    );

    try {
      final response = await http.post(
        url,
        headers: {'accept': '*/*'},
        body: '',
      );

      print("Response status: ${response.statusCode}");
      print("Response body: ${response.body}");

      setState(() => _loading = false);

      if (response.statusCode == 200) {
        final json = jsonDecode(response.body);
        final token = json['token'];
        print("Token: $token");
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('Login successful')));
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (_) => HomeScreen()),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(
              'Login failed: ${response.statusCode}\nDetails: ${response.body}',
            ),
            duration: const Duration(seconds: 5),
          ),
        );
      }
    } catch (e, stacktrace) {
      setState(() => _loading = false);
      print("Exception during login: $e");
      print(stacktrace);
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Login exception: $e')));
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final size = MediaQuery.of(context).size;

    return Scaffold(
      body: Stack(
        children: [
          Positioned.fill(
            child: Opacity(
              opacity: 0.4,
              child: Image.asset(
                'assets/backgroundimage.png',
                fit: BoxFit.cover,
              ),
            ),
          ),
          SafeArea(
            child: SingleChildScrollView(
              padding: const EdgeInsets.symmetric(
                horizontal: 24.0,
                vertical: 32,
              ),
              child: ConstrainedBox(
                constraints: BoxConstraints(
                  minHeight: size.height - MediaQuery.of(context).padding.top,
                ),
                child: IntrinsicHeight(
                  child: Container(
                    decoration: BoxDecoration(
                      color: Colors.white.withOpacity(0.85),
                      borderRadius: BorderRadius.circular(24),
                    ),
                    padding: const EdgeInsets.all(24),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const SizedBox(height: 32),
                        Text(
                          "Login Account",
                          style: theme.textTheme.headlineMedium?.copyWith(
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        const SizedBox(height: 8),
                        Text(
                          "Hello, you must login first to be able to use the app and enjoy all features.",
                          style: theme.textTheme.bodyMedium?.copyWith(
                            color: Colors.grey[700],
                          ),
                        ),
                        const SizedBox(height: 32),
                        TextField(
                          controller: _emailController,
                          decoration: InputDecoration(
                            labelText: "Email Address",
                            border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(12),
                            ),
                          ),
                          keyboardType: TextInputType.emailAddress,
                        ),
                        const SizedBox(height: 16),
                        TextField(
                          controller: _passwordController,
                          obscureText: _obscure,
                          decoration: InputDecoration(
                            labelText: "Password",
                            border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(12),
                            ),
                            suffixIcon: IconButton(
                              icon: Icon(
                                _obscure
                                    ? Icons.visibility_off
                                    : Icons.visibility,
                              ),
                              onPressed: () {
                                setState(() => _obscure = !_obscure);
                              },
                            ),
                          ),
                        ),
                        const SizedBox(height: 8),
                        Align(
                          alignment: Alignment.centerRight,
                          child: TextButton(
                            onPressed: () {}, // TODO: forgot password logic
                            child: const Text("Forgot Password?"),
                          ),
                        ),
                        const SizedBox(height: 16),
                        SizedBox(
                          width: double.infinity,
                          height: 48,
                          child: ElevatedButton(
                            style: ElevatedButton.styleFrom(
                              backgroundColor: Colors.lightBlue,
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(24),
                              ),
                            ),
                            onPressed: _loading ? null : _login,
                            child: _loading
                                ? const CircularProgressIndicator(
                                    color: Colors.white,
                                  )
                                : const Text(
                                    "Sign In",
                                    style: TextStyle(color: Colors.white),
                                  ),
                          ),
                        ),
                        const Spacer(),
                        Center(
                          child: TextButton(
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (_) => const RegisterScreen(),
                                ),
                              );
                            },
                            child: const Text.rich(
                              TextSpan(
                                text: "Don't have an account? ",
                                children: [
                                  TextSpan(
                                    text: "Join Us",
                                    style: TextStyle(color: Colors.orange),
                                  ),
                                ],
                              ),
                            ),
                          ),
                        ),
                        const SizedBox(height: 16),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
