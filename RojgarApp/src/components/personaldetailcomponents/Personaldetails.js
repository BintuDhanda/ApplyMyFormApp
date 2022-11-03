import React, {useContext, useEffect, useState} from 'react';
import {
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import {Button, TextInput} from 'react-native-paper';
import {AppContext} from '../../settings/Context';
import Colors from '../../settings/Colors';
import {CFetch, CFormFetch} from '../../settings/APIFetch';
import Loader from '../../utilities/Loader';

const Personaldetails = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [editing, setEditing] = useState(false);
  const [model, setModel] = useState({
    id: 0,
    name: '',
    fatherName: '',
    motherName: '',
    email: '',
    phoneNumber: '',
    alternateNumber: '',
    aadhaarNumber: '',
    gender: '',
    address1: '',
    address2: '',
    country: '',
    state: '',
    city: '',
    landMark: '',
    pinCode: '',
    // photo: '',
    // photoUrl: '',
    // sign: '',
    // signUrl: '',
  });
  const handleInput = (name, value) => {
    if (name === 'photo' || name === 'sign') {
      setModel({
        ...model,
        [name]: value,
        [name + 'Url']: value.uri,
      });
    } else {
      setModel({
        ...model,
        [name]: value,
      });
    }
  };
  // const onSelectFile = name => {
  //   DocumentPicker.pick({type: 'image/*'}).then(res => {
  //     if (DocumentPicker.isCancel()) {
  //       console.log('Cancelled');
  //     } else {
  //       handleInput(name, {
  //         name: res[0].name,
  //         uri: res[0].uri,
  //         type: res[0].type,
  //       });
  //     }
  //   });
  // };
  useEffect(() => {
    loadData();
  }, []);
  
  const loadData = () => {
    CFetch('PersonalDetail', appUser.token, {})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (result) {
              setModel(result);
              setEditing(true);
            }
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

  const handleSubmit = () => {
    var formData = new FormData();
    if (editing) {
      formData.append('id', model.id);
    }
    formData.append('name', model.name);
    formData.append('fatherName', model.fatherName);
    formData.append('motherName', model.motherName);
    formData.append('email', model.email);
    formData.append('phoneNumber', model.phoneNumber);
    formData.append('alternateNumber', model.alternateNumber);
    formData.append('aadhaarNumber', model.aadhaarNumber);
    formData.append('gender', model.gender);
    formData.append('address1', model.address1);
    formData.append('address2', model.address2);
    formData.append('country', model.country);
    formData.append('state', model.state);
    formData.append('city', model.city);
    formData.append('pinCode', model.pinCode);
    formData.append('landMark', model.landMark);
    // if (model.photo) {
    //   formData.append('photo', model.photo);
    // }
    // if (model.sign) {
    //   formData.append('sign', model.sign);
    // }
    setLoading(true);
    setWaiting(true);
    let url = editing ? 'UpdatePersonalDetail' : 'CreatePersonalDetail';
    CFormFetch(url, appUser.token, formData)
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            alert(result);
          });
        }
      })
      .catch(error => {
        console.warn(error);
      })
      .finally(() => {
        loadData();
      });
  };
  return (
    <ScrollView keyboardShouldPersistTaps="handled">
      <View style={styles.container}>
        <Loader loading={loading} waiting={waiting} spinner={true} />
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('name', val)}
            value={model.name}
            style={styles.input}
            placeholder="Enter Your Name"
            label="Full Name"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('fatherName', val)}
            value={model.fatherName}
            style={styles.input}
            placeholder="Enter Your Father Name"
            label="Father Name"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('motherName', val)}
            value={model.motherName!="null"?model.motherName:null}
            style={styles.input}
            placeholder="Enter Your Mother Name"
            label="Mother Name"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('phoneNumber', val)}
            value={model.phoneNumber!="null"?model.phoneNumber:null}
            style={styles.input}
            placehol
            der="Enter Your  Phone Number"
            label="Phone Number"
            keyboardType="phone-pad"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('alternateNumber', val)}
            value={model.alternateNumber!="null"?model.alternateNumber:null}
            style={styles.input}
            placeholder="Enter Your Alternate Phone Number"
            label="Alternate Phone Number"
            keyboardType="phone-pad"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('email', val)}
            value={model.email!="null"?model.email:null}
            style={styles.input}
            placeholder="Enter Your Email"
            label="Email"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('aadhaarNumber', val)}
            value={model.aadhaarNumber!="null"?model.aadhaarNumber:null}
            style={styles.input}
            placeholder="Enter Your Aadhaar Number"
            label="Aadhaar Number"
            keyboardType="number-pad"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('address1', val)}
            value={model.address1!="null"?model.address1:null}
            style={styles.input}
            placeholder="Enter Your Address 1"
            label="Address 1"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('address2', val)}
            value={model.address2!="null"?model.address2: null}
            style={styles.input}
            placeholder="Enter Your Address 2"
            label="Address 2"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('city', val)}
            value={model.city!="null"?model.city:null}
            style={styles.input}
            placeholder="Enter Your City"
            label="City"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('state', val)}
            value={model.state!="null"?model.state:null}
            style={styles.input}
            placeholder="Enter Your State"
            label="State"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('country', val)}
            value={model.country!="null"?model.country:null}
            style={styles.input}
            placeholder="Enter Your Country"
            label="Country"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('landMark', val)}
            value={model.landMark!="null"?model.landMark:null}
            style={styles.input}
            placeholder="Enter Your Landmark"
            label="Landmark"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        <View style={styles.inputContainer}>
          <TextInput
            onChangeText={val => handleInput('pinCode', val)}
            value={model.pinCode!="null"?model.pinCode:null}
            style={styles.input}
            placeholder="Enter Your Pincode"
            label="Pincode"
            keyboardType="number-pad"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
          />
        </View>
        {/* <TouchableOpacity
          onPress={() => onSelectFile('photo')}
          style={styles.inputContainer}>
          <TextInput
            value={model.photo ? model.photo.name : ''}
            style={styles.input}
            placeholder="Photo"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
            editable={false}
          />
          <Text style={styles.filePickerLabel}>Choose Photo</Text>
        </TouchableOpacity>
        {model.photoUrl ? (
          <FastImage
            source={{uri: model.photoUrl}}
            style={{width: 100, height: 100}}
            resizeMode={FastImage.resizeMode.center}
          />
        ) : null}
        <TouchableOpacity
          onPress={() => onSelectFile('sign')}
          style={styles.inputContainer}>
          <TextInput
            value={model.sign ? model.sign.name : ''}
            style={styles.input}
            placeholder="Sign"
            mode="outlined"
            outlineColor="#4CA6A8"
            activeOutlineColor="#4CA6A8"
            editable={false}
          />
          <Text style={styles.filePickerLabel}>Choose Sign</Text>
        </TouchableOpacity>
        {model.signUrl ? (
          <FastImage
            source={{uri: model.signUrl}}
            style={{width: 100, height: 100}}
            resizeMode={FastImage.resizeMode.center}
          />
        ) : null} */}
        <Button
          onPress={handleSubmit}
          style={styles.btn}
          labelStyle={styles.btnLabel}>
          {editing ? 'Update' : 'Save'}
        </Button>
      </View>
    </ScrollView>
  );
};

export default Personaldetails;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 25,
    paddingVertical: 30
  },
  inputContainer: {
    marginBottom: 25
  },
  input: {
    height: 45,
  },
  filePickerLabel: {
    position: 'absolute',
    color: Colors.white,
    right: 0,
    borderRadius: 3,
    top: 5,
    backgroundColor: '#4CA6A8',
    padding: 15,
  },
  btn: {
    backgroundColor: '#4CA6A8',
    marginTop: 10,
    padding: 5,
  },
  btnLabel: {
    color: Colors.white,
  },
});
