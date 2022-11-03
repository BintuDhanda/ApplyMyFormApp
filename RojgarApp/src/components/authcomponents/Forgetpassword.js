import {useNavigation} from '@react-navigation/native';
import React, {useContext, useState} from 'react';
import {View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import FastImage from 'react-native-fast-image';
import {Button, TextInput} from 'react-native-paper';
import {CFetch} from '../../settings/APIFetch';
import {AuthContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';
import Snackbar from 'react-native-snackbar';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';

const Login = () => {
  const {signIn} = useContext(AuthContext);
  const navigation = useNavigation();
  const [UserName, SetUserName] = useState();
  const [loading, setLoading] = useState(false);
  const [waiting, setWaiting] = useState(false);
  const Forgetpassword = () => {
    if (!UserName) {
      Snackbar.show({
        text: 'Please enter username',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    setWaiting(true);
    setLoading(true);
    CFetch('ForgetPassword', null, {UserName: UserName})
      .then(res => {
        console.log(res.status);
        if (res.status == 200) {
          navigation.navigate('ResetPassword', {userName: UserName});
        } else if (res.status === 441) {
          res.json().then(result => {
            setTimeout(() => {
              Snackbar.show({
                text: result,
                duration: Snackbar.LENGTH_LONG,
              });
            }, 10);
          });
        } else if (res.status === 406) {
          res.json().then(result => {
            navigation.navigate('ConfirmAccount', {userName: UserName});
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
      <FastImage
        source={require('../../assets/images/logo-2.jpeg')}
        style={{width: '100%', height: 180}}
        resizeMode={FastImage.resizeMode.contain}
      />
      <View style={styles.titlecontainer}>
        <Text style={styles.Weltitle}>Welcome Back !</Text>
        <Text style={styles.desctitle}>Forget your password</Text>
      </View>

      <View style={{width: '85%'}}>
        <TextInput
          style={styles.input}
          label="Enter Phone Number"
          keyboardType="phone-pad"
          onChangeText={val => SetUserName(val)}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
        />
      </View>
      <Button
        style={styles.loginbutton}
        onPress={Forgetpassword}
        labelStyle={styles.btnlable}>
        Forget Password
      </Button>
      <View style={styles.endtextcontainer}>
        <Text style={styles.txt1}>Go back?</Text>
        <TouchableOpacity onPress={() => navigation.navigate('Login')}>
          <Text style={styles.txt2}>Login</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
export default Login;

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

  input: {
    width: '100%',
    marginTop: 15,
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
