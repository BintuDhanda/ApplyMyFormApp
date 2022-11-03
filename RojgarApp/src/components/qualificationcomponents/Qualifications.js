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
} from 'react-native';
import {Button, TextInput, Title} from 'react-native-paper';
import * as DocumentPicker from 'react-native-document-picker';
import {Picker} from '@react-native-picker/picker';
import MaterialCommunityIcons from 'react-native-vector-icons/dist/MaterialCommunityIcons';
import Snackbar from 'react-native-snackbar';

import {AppContext} from '../../settings/Context';
import Colors from '../../settings/Colors';
import {CFetch, CFormFetch} from '../../settings/APIFetch';
import Loader from '../../utilities/Loader';
import QualificationItem from './QualificationItem';
const Qualifications = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const [editing, setEditing] = useState(false);
  const [visible, setVisible] = useState(false);
  const [data, setData] = useState([]);
  const take = 20;
  const [skip, setSkip] = useState(take);
  const qualifications = [
    {label: 'Middle', value: 'Middle'},
    {label: 'Secondary', value: 'Secondary'},
    {label: 'Senior Secondary', value: 'Senior Secondary'},
    {label: 'Diploma / ITI / Polytechnic', value: 'Diploma / ITI / Polytechnic'},
    {label: 'Graduation', value: 'Graduation'},
    {label: 'Post Graduation', value: 'Post Graduation'},
    {label: 'Phd.', value: 'Phd.'},
    {label: 'Special Qualification', value: 'Special Qualification'},
  ];
  const [model, setModel] = useState({
    id: '',
    name: '',
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
    CFetch('Qualifications', appUser.token, {
      skip: isSkip ? skip : 0,
      take: take,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (isSkip) {
              setSkip(skip + take);
              setData([...data, ...result]);
            } else {
              setSkip(take);
              setData(result);
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
      formData.append('Id', model.id);
    }
    formData.append('name', model.name);
    if (model.file) {
      formData.append('file', model.file);
    }

    let url = editing ? 'UpdateQualification' : 'CreateQualification';
    CFormFetch(url, appUser.token, formData)
      .then(res => {
        console.log(res.status);
        if (res.status === 200) {
          setVisible(false);
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
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
    CFetch('GetQualificationById', appUser.token, {Id: id})
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
    CFetch('DeleteQualification', appUser.token, {Id: id})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
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
            + Add Qualification
          </Button>
        </View>
        <FlatList
          data={data}
          refreshControl={
            <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
          }
          keyExtractor={(_, index) => index.toString()}
          onEndReached={() => loadData(true)}
          onEndReachedThreshold={0.2}
          renderItem={({item, index}) => (
            <QualificationItem
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
              {editing ? 'Update Qualification' : 'Add New Qualification'}
            </Title>
            <Pressable onPress={() => setVisible(false)}>
              <MaterialCommunityIcons name="close" size={25} />
            </Pressable>
          </View>
          <View style={[styles.pickerContainer, styles.inputContainer]}>
            <Picker
              style={styles.input}
              selectedValue={model.name}
              onValueChange={val => {
                handleInput('name', val);
              }}>
              <Picker.Item label="-- Select Qualification* --" value="" />
              {qualifications.map((item, index) => (
                <Picker.Item
                  key={index}
                  label={item.label}
                  value={item.value}
                />
              ))}
            </Picker>
          </View>
          <TouchableOpacity
            onPress={() => onSelectFile('file')}
            style={styles.inputContainer}>
            <TextInput
              value={model.file ? model.file.name : ''}
              theme={{colors: {primary: Colors.green}}}
              style={styles.input}
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

export default Qualifications;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  inputContainer: {
    marginBottom: 10,
  },
  input: {
    height: 45,
    marginBottom: 5,
  },
  pickerContainer: {
    paddingVertical: 2,
    borderRadius: 5,
    borderColor: Colors.transparentBlack,
    borderWidth: 1,
  },
  filePickerLabel: {
    position: 'absolute',
    color: Colors.white,
    right: 2,
    borderRadius: 3,
    top: 7,
    backgroundColor: '#4CA6A8',
    padding: 13,
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
  btn: {
    backgroundColor: '#4CA6A8',
    padding: 5,
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
