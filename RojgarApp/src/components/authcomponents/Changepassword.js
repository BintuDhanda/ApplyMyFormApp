import React, {useContext, useState} from 'react';
import {View, StyleSheet} from 'react-native';
import {TextInput, Button} from 'react-native-paper';
import {CFetch} from '../../settings/APIFetch';
import Snackbar from 'react-native-snackbar';
import Loader from '../../utilities/Loader';
import {AppContext} from '../../settings/Context';
import {useNavigation} from '@react-navigation/native';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';

const Changepassword = () => {
  const navigation = useNavigation();
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(false);
  const [waiting, setWaiting] = useState(false);

  const [model, setModel] = useState({
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  });
  const [secureText, setSecureText] = useState(true);
  const [secureTextNew, setSecureTextNew] = useState(true);
  const handleChangePassword = () => {
    if (!model.oldPassword) {
      Snackbar.show({
        text: 'Please enter old password',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    if (!model.newPassword) {
      Snackbar.show({
        text: 'Please enter new password',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    if (model.newPassword != model.confirmPassword) {
      Snackbar.show({
        text: 'Password not matched',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    setLoading(true);
    setWaiting(true);
    CFetch('ChangePassword', appUser.token, model)
      .then(res => {
        if (res.status === 200) {
          setTimeout(() => {
            Snackbar.show({
              text: 'Password Changed Successfully',
              duration: Snackbar.LENGTH_LONG,
            });
          }, 400);
          setTimeout(() => {
            navigation.goBack();
          }, 410);
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
      <View style={{width: '85%'}}>
        <TextInput
          style={styles.otpinput}
          label={'Enter Old Password'}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
          secureTextEntry={secureText}
          value={model.oldPassword}
          onChangeText={v => setModel({...model, oldPassword: v})}
        />
        <FontAwesome
          name={secureText ? 'eye-slash' : 'eye'}
          size={20}
          style={styles.eye}
          onPress={() => setSecureText(!secureText)}
        />
      </View>
      <View style={{width: '85%'}}>
        <TextInput
          style={styles.otpinput}
          label={'Enter New Password'}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
          secureTextEntry={secureTextNew}
          value={model.newPassword}
          onChangeText={v => setModel({...model, newPassword: v})}
        />
        <FontAwesome
          name={secureTextNew ? 'eye-slash' : 'eye'}
          size={20}
          style={styles.eye}
          onPress={() => setSecureTextNew(!secureTextNew)}
        />
      </View>
      <View style={{width: '85%'}}>
        <TextInput
          style={styles.otpinput}
          label={'Enter Confirm Password'}
          mode="outlined"
          outlineColor="#4CA6A8"
          activeOutlineColor="#4CA6A8"
          secureTextEntry={secureTextNew}
          value={model.confirmPassword}
          onChangeText={v => setModel({...model, confirmPassword: v})}
        />
        <FontAwesome
          name={secureTextNew ? 'eye-slash' : 'eye'}
          size={20}
          style={styles.eye}
          onPress={() => setSecureTextNew(!secureTextNew)}
        />
      </View>
      <Button
        style={styles.loginbutton}
        labelStyle={styles.btnlable}
        onPress={handleChangePassword}>
        Change Password
      </Button>
    </View>
  );
};

export default Changepassword;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  otpinput: {
    width: '100%',
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
  eye: {
    position: 'absolute',
    top: '40%',
    padding: 5,
    right: 15,
    zIndex: 9,
  },
});
