import {useNavigation} from '@react-navigation/native';
import React, {useState} from 'react';
import {View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import {Button, TextInput} from 'react-native-paper';
import {CFetch} from '../../settings/APIFetch';
import Snackbar from 'react-native-snackbar';
import Loader from '../../utilities/Loader';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';

const Signup = () => {
  const navigation = useNavigation();
  const [loading, setLoading] = useState(false);
  const [waiting, setWaiting] = useState(false);
  const [secureText, setSecureText] = useState(true);
  const [Password, SetPassword] = useState();
  const [model, setModel] = useState({
    fullName: '',
    phoneNumber: '',
    passwordHash: '',
  });
  const handleInput = (name, value) => {
    setModel({
      ...model,
      [name]: value,
    });
  };
  const Register = () => {
    if (!model.phoneNumber || !model.fullName || !model.passwordHash) {
      Snackbar.show({
        text: 'Please fill all fields',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    if (Password != model.passwordHash) {
      alert('Password not matched');
      return false;
    }
    setLoading(true);
    setWaiting(true);
    CFetch('SignUp', null, model)
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            navigation.navigate('ConfirmAccount', {userName: result.userName});
          });
        } else if (res.status === 441) {
          res.json().then(result => {
            setTimeout(() => {
              Snackbar.show({
                text: result,
                duration: Snackbar.LENGTH_LONG,
              });
            }, 10);
          });
        } else {
          setTimeout(() => {
            Snackbar.show({
              text: 'Something went wrong',
              duration: Snackbar.LENGTH_LONG,
            });
          }, 10);
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setLoading(false);
        setWaiting(false);
      });
  };
  return (
    <View style={styles.container}>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      <View style={styles.titlecontainer}>
        <Text style={styles.Weltitle}>Register Account</Text>
        <Text style={styles.desctitle}>Fill your details or continue</Text>
      </View>
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          label="Enter Your Name"
          onChangeText={val => handleInput('fullName', val)}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
        />
      </View>
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          label="Enter Your Mobile No."
          keyboardType="number-pad"
          onChangeText={val => handleInput('phoneNumber', val)}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
        />
      </View>
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          label="Enter Your Password"
          secureTextEntry={secureText}
          onChangeText={val => handleInput('passwordHash', val)}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
        />
        <FontAwesome
          name={secureText ? 'eye-slash' : 'eye'}
          size={20}
          style={styles.eye}
          onPress={() => setSecureText(!secureText)}
        />
      </View>
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          label="Confirm Your Password"
          secureTextEntry={secureText}
          onChangeText={val => SetPassword(val)}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
        />
        <FontAwesome
          name={secureText ? 'eye-slash' : 'eye'}
          size={20}
          style={styles.eye}
          onPress={() => setSecureText(!secureText)}
        />
      </View>
      <Button
        onPress={Register}
        style={styles.loginbutton}
        labelStyle={styles.btnlable}>
        Sign Up
      </Button>
      <View style={styles.endtextcontainer}>
        <Text style={styles.txt1}>Already have an account?</Text>
        <TouchableOpacity onPress={() => navigation.navigate('Login')}>
          <Text style={styles.txt2}> Log In </Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
export default Signup;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    alignItems: 'center',
    justifyContent: 'center',
  },
  titlecontainer: {
    width: '85%',
    paddingBottom: 5,
  },
  Weltitle: {
    color: 'black',
    fontWeight: '700',
    fontSize: 35,
  },
  desctitle: {
    fontWeight: '400',
    fontSize: 16,
  },
  inputContainer: {
    width: '85%',
  },
  input: {
    width: '100%',
    marginTop: 10,
  },
  eye: {
    position: 'absolute',
    top: '40%',
    padding: 5,
    right: 15,
    zIndex: 9,
  },
  loginbutton: {
    backgroundColor: '#4CA6A8',
    width: '85%',
    marginTop: 30,
    padding: 5,
  },
  btnlable: {
    color: 'white',
  },
  endtextcontainer: {
    marginTop: 20,
    flexDirection: 'row',
    padding: 2,
  },
  txt1: {
    color: 'black',
    fontSize: 16,
    marginRight: 5,
  },
  txt2: {
    color: '#4CA6A8',
    fontSize: 16,
  },
});
