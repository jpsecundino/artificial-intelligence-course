#include<bits/stdc++.h>

using namespace std;

#define WALL '#'

bool bfs(const vector<vector<char>>& grid, 
         const pair<int, int> target,
         vector<vector<bool>>& visited, 
         queue<pair<int,int>>& searchTree, 
         map<pair<int, int>, pair<int,int>>& prev,
         int i, 
         int j){


    queue<pair<int, int>> traversalOrder;

    traversalOrder.push(make_pair(i,j));
    prev[make_pair(i, j)] = make_pair(-1, -1);
    visited[i][j] = true;

    while(!traversalOrder.empty()){
        
        pair<int,int> curPos = traversalOrder.front();
        traversalOrder.pop();

        int i = curPos.first,
            j = curPos.second;

        
        if(i == target.first && j == target.second){
            return true;
        }

        searchTree.push(make_pair(i,j));
        
        int moves[4][2] = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};

        for (int k = 0; k < 4; k++)
        {
            int new_i = i + moves[k][0],
                new_j = j + moves[k][1];

            if (new_i >= 0 && new_i < grid.size() && new_j >= 0 && new_j < grid[0].size()){
                if(grid[new_i][new_j] != WALL && !visited[new_i][new_j]){
                    visited[new_i][new_j] = true;
                    
                    pair<int, int> newPos = make_pair(new_i, new_j);
                    
                    traversalOrder.push(newPos);
                    
                    prev[newPos] = curPos;
                }
            }
            
        }
    }

    return false;
}

int main(){
    int n, m;

    cin >> n >> m;

    vector<vector<char>> grid(n, vector<char>(m));
    vector<vector<bool>> visited(n, vector<bool>(m, false));

    pair<int, int> startPos;
    pair<int, int> targetPos;

    for (int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++){
            cin >> grid[i][j];

            if(grid[i][j] == 's'){
                startPos = make_pair(i, j);
            } 

            if(grid[i][j] == 't'){
                targetPos = make_pair(i, j);
            } 
        }
    }

    queue<pair<int,int>> searchTree;
    map<pair<int, int>, pair<int, int>> prev;
    stack<pair<int, int>> truePath;

    if(bfs(grid, targetPos, visited, searchTree, prev, startPos.first, startPos.second)){
        while (!searchTree.empty())
        {
            pair<int, int> pos = searchTree.front();
            searchTree.pop();
            cout << pos.first << " " << pos.second << endl;
        }

        pair<int, int> aux = targetPos;
        while(prev[aux] != make_pair(-1,-1)){
            truePath.push(aux);
            aux = prev[aux];
        }

        truePath.push(startPos);

        cout << "tp" << endl;

        while (!truePath.empty())
        {
            cout << truePath.top().first << " " << truePath.top().second << endl;
            truePath.pop();
        }
              
    }

    return 0;
}