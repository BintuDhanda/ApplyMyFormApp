import React, {useCallback, useContext, useEffect, useState} from 'react';
import {
  StyleSheet,
  Text,
  Modal,
  TouchableOpacity,
  View,
  Pressable,
  FlatList,
  RefreshControl,
  Platform,
} from 'react-native';
import {Button, TextInput, Title} from 'react-native-paper';
import * as DocumentPicker from 'react-native-document-picker';
import MaterialCommunityIcons from 'react-native-vector-icons/dist/MaterialCommunityIcons';
import Snackbar from 'react-native-snackbar';
import DateTimePicker from '@react-native-community/datetimepicker';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';

import {AppContext} from '../../settings/Context';
import Colors from '../../settings/Colors';
import {CFetch, CFormFetch} from '../../settings/APIFetch';
import Loader from '../../utilities/Loader';
import ExperienceItem from './ExperienceItem';
import {useFocusEffect} from '@react-navigation/native';
const Experiences = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const [editing, setEditing] = useState(false);
  const [visible, setVisible] = useState(false);
  const [data, setData] = useState([]);
  const take = 20;
  const [skip, setSkip] = useState(take);
  const [model, setModel] = useState({
    id: '',
    name: '',
    salary: '',
    joinDate: '',
    leaveDate: '',
    file: '',
    fileUrl: '',
  });
  const handleInput = (name, value) => {
    setModel({
      ...model,
      [name]: value,
    });
  };
  const onSelectFile = name => {
    DocumentPicker.pick({}).then(res => {
      if (DocumentPicker.isCancel()) {
        console.log('Cancelled');
      } else {
        handleInput(name, {
          name: res[0].name,
          uri: res[0].uri,
          type: res[0].type,
        });
      }
    });
  };
  const [showDatePicker, setShowDatePicker] = useState('');
  const handleShowDatePicker = picker => {
    setShowDatePicker(picker);
  };
  const onChangeDate = (event, name, date) => {
    setShowDatePicker('');
    if (Platform.OS === 'ios') {
      handleInput(name, date);
    } else if (event.type === 'set') {
      handleInput(name, date);
    }
  };

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setWaiting(true);
    setLoading(true);
    loadData();
  });

  useEffect(() => {
    setLoading(true);
    loadData();
  }, []);

  const loadData = isSkip => {
    CFetch('Experiences', appUser.token, {skip: isSkip ? skip : 0, take: take})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (result) {
              if (isSkip) {
                setSkip(skip + take);
                setData([...data, ...result]);
              } else {
                setSkip(take);
                setData(result);
              }
            }
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setEditing(false);
        setLoading(false);
        setWaiting(false);
        setRefreshing(false);
      });
  };

  const handleSubmit = () => {
    var formData = new FormData();
    if (editing) {
      formData.append('id', model.id);
    }
    formData.append('name', model.name);
    formData.append('salary', parseInt(model.salary));
    formData.append('joinDate', new Date(model.joinDate).toISOString());
    formData.append('leaveDate', new Date(model.leaveDate).toISOString());
    if (model.file) {
      formData.append('file', model.file);
    }

    let url = editing ? 'UpdateExperience' : 'CreateExperience';
    CFormFetch(url, appUser.token, formData)
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
            setVisible(false);
          });
        } else if (res.status === 441) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
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
  const handleEdit = id => {
    setLoading(true);
    setWaiting(true);
    CFetch('GetExperienceById', appUser.token, {Id: id})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setModel(result);
            setEditing(true);
            setVisible(true);
          });
        } else if (res.status === 441) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setWaiting(false);
        setLoading(false);
      });
  };
  const handleDelete = id => {
    setWaiting(true);
    setLoading(true);
    CFetch('DeleteExperience', appUser.token, {Id: id})
      .then(res => {
        if (res.status === 200) {
          Snackbar.show({
            text: result,
            duration: Snackbar.LENGTH_LONG,
          });
        } else if (res.status === 441) {
          Snackbar.show({
            text: result,
            duration: Snackbar.LENGTH_LONG,
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
          });
        }
      })
      .finally(() => {
        loadData();
      });
  };
  return (
    <>
      <View style={styles.container}>
        <Loader loading={loading} waiting={waiting} spinner={true} />
        <View style={styles.btnrow}>
          <Button
            style={styles.addbtn}
            labelStyle={styles.btnLabel}
            onPress={() => setVisible(true)}>
            + Add Experience
          </Button>
        </View>
        <FlatList
          data={data}
          keyExtractor={(_, index) => index.toString()}
          refreshControl={
            <RefreshControl onRefresh={onRefresh} refreshing={refreshing} />
          }
          onEndReached={() => loadData(true)}
          onEndReachedThreshold={0.2}
          renderItem={({item, index}) => (
            <ExperienceItem
              item={item}
              index={index}
              handleEdit={handleEdit}
              handleDelete={handleDelete}
            />
          )}
        />
      </View>
      <Modal
        animationType="slide"
        visible={visible}
        onRequestClose={() => setVisible(false)}>
        <View style={styles.modalContainer}>
          <View style={styles.modalTitle}>
            <Title>
              {editing ? 'Update Experience' : 'Add New Experience'}
            </Title>
            <Pressable onPress={() => setVisible(false)}>
              <MaterialCommunityIcons name="close" size={25} />
            </Pressable>
          </View>
          <View style={styles.inputContainer}>
            <TextInput
              onChangeText={val => handleInput('name', val)}
              style={styles.input}
              placeholder="Enter Department Name"
              label="Department Name"
              mode="outlined"
              outlineColor={Colors.primary}
              activeOutlineColor={Colors.primary}
            />
            <TextInput
              onChangeText={val => handleInput('salary', val)}
              style={styles.input}
              placeholder="Enter Salary"
              label="Last Salary"
              mode="outlined"
              keyboardType="phone-pad"
              outlineColor={Colors.primary}
              activeOutlineColor={Colors.primary}
            />
            <View>
              <TouchableOpacity
                onPress={() => handleShowDatePicker('joinDate')}>
                <TextInput
                  onChangeText={val => handleInput('joinDate', val)}
                  value={
                    model.joinDate
                      ? new Date(model.joinDate).toDateString()
                      : ''
                  }
                  style={styles.input}
                  placeholder="Enter Join Date"
                  label="Join Date"
                  mode="outlined"
                  keyboardType="phone-pad"
                  outlineColor={Colors.primary}
                  activeOutlineColor={Colors.primary}
                  editable={false}
                />
                <FontAwesome name="calendar" style={styles.calendarIcon} />
              </TouchableOpacity>
              {showDatePicker === 'joinDate' && (
                <DateTimePicker
                  testID="dateTimePicker"
                  value={model.joinDate ? new Date(model.joinDate) : new Date()}
                  mode="date"
                  is24Hour={true}
                  display="default"
                  onChange={(event, date) =>
                    onChangeDate(event, 'joinDate', date)
                  }
                />
              )}
            </View>
            <View>
              <TouchableOpacity
                onPress={() => handleShowDatePicker('leaveDate')}>
                <TextInput
                  onChangeText={val => handleInput('leaveDate', val)}
                  value={
                    model.leaveDate
                      ? new Date(model.leaveDate).toDateString()
                      : ''
                  }
                  style={styles.input}
                  placeholder="Enter Leave Date"
                  label="Leave Date"
                  mode="outlined"
                  keyboardType="phone-pad"
                  outlineColor={Colors.primary}
                  activeOutlineColor={Colors.primary}
                  editable={false}
                />
                <FontAwesome name="calendar" style={styles.calendarIcon} />
              </TouchableOpacity>
              {showDatePicker === 'leaveDate' && (
                <DateTimePicker
                  testID="dateTimePicker"
                  value={
                    model.leaveDate ? new Date(model.leaveDate) : new Date()
                  }
                  mode="date"
                  is24Hour={true}
                  display="default"
                  onChange={(event, date) =>
                    onChangeDate(event, 'leaveDate', date)
                  }
                  minimumDate={
                    model.joinDate ? new Date(model.joinDate) : new Date()
                  }
                />
              )}
            </View>
          </View>
          <TouchableOpacity
            onPress={() => onSelectFile('file')}
            style={styles.inputContainer}>
            <TextInput
              value={model.file ? model.file.name : ''}
              style={styles.inputfile}
              placeholder="File"
              mode="outlined"
              outlineColor={Colors.primary}
              activeOutlineColor={Colors.primary}
              editable={false}
            />
            <Text style={styles.filePickerLabel}>Choose File</Text>
          </TouchableOpacity>
          <Button
            onPress={handleSubmit}
            style={styles.btn}
            labelStyle={styles.btnLabel}>
            {editing ? 'Update' : 'Save'}
          </Button>
        </View>
      </Modal>
    </>
  );
};

export default Experiences;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  btnrow: {
    alignItems: 'center',
  },
  addbtn: {
    marginTop: 15,
    backgroundColor: '#4CA6A8',
    color: 'white',
    width: '70%',
  },
  inputContainer: {
    marginBottom: 10,
  },
  input: {
    height: 45,
    marginTop: 13,
  },
  inputfile: {
    height: 45,
  },
  filePickerLabel: {
    position: 'absolute',
    color: Colors.white,
    right: 0,
    borderRadius: 3,
    top: 7,
    backgroundColor: '#4CA6A8',
    padding: 14,
  },
  calendarIcon: {
    position: 'absolute',
    right: 10,
    top: '50%',
    fontSize: 20,
    color: '#4CA6A8',
  },
  btn: {
    backgroundColor: '#4CA6A8',
    padding: 3,
  },
  btnLabel: {
    color: Colors.white,
  },
  modalContainer: {
    padding: 10,
  },
  modalTitle: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 5,
    justifyContent: 'space-between',
    marginBottom: 10,
  },
});
