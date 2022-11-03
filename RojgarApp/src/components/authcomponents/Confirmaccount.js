import {useNavigation} from '@react-navigation/native';
import React, {useEffect, useState} from 'react';
import {View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import {TextInput, Button} from 'react-native-paper';
import {CFetch} from '../../settings/APIFetch';
import Snackbar from 'react-native-snackbar';
import Loader from '../../utilities/Loader';

const Confirmaccount = ({route}) => {
  const navigation = useNavigation();
  const [loading, setLoading] = useState(false);
  const [waiting, setWaiting] = useState(false);
  const [model, setModel] = useState({
    userName: '',
    Otp: '',
  });
  useEffect(() => {
    setModel({
      ...model,
      userName: route.params.userName,
    });
  }, []);
  const handleConfirmAccount = () => {
    if (!model.Otp) {
      Snackbar.show({
        text: 'Please enter OTP',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    setLoading(true);
    setWaiting(true);
    CFetch('ConfirmAccount', null, model)
      .then(res => {
        if (res.status === 200) {
          setTimeout(() => {
            Snackbar.show({
              text: 'Account Confirmed',
              duration: Snackbar.LENGTH_LONG,
            });
          }, 10);
          navigation.navigate('Login');
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
  const handleResendOTP = () => {
    setLoading(true);
    setWaiting(true);
    CFetch('ResendOTP', null, {UserName: model.userName})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setTimeout(() => {
              Snackbar.show({
                text: result,
                duration: Snackbar.LENGTH_LONG,
              });
            }, 10);
          });
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
      <TextInput
        style={styles.otpinput}
        label={'Enter OTP'}
        mode="outlined"
        outlineColor="#4CA6A8"
        activeOutlineColor="#4CA6A8"
        onChangeText={v => setModel({...model, Otp: parseInt(v)})}
      />
      <View style={{width: '85%', marginTop: 10}}>
        <TouchableOpacity
          style={{marginLeft: 'auto'}}
          onPress={handleResendOTP}>
          <Text>Resend OTP</Text>
        </TouchableOpacity>
      </View>
      <Button
        style={styles.loginbutton}
        labelStyle={styles.btnlable}
        onPress={handleConfirmAccount}>
        Submit OTP
      </Button>
    </View>
  );
};

export default Confirmaccount;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  otpinput: {
    width: '85%',
    alignSelf: 'center',
  },
  loginbutton: {
    backgroundColor: '#4CA6A8',
    width: '85%',
    marginTop: 20,
    padding: 5,
  },
  btnlable: {
    color: 'white',
  },
});
